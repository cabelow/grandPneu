import { Component, Input } from '@angular/core';
import { IonicModule, ModalController, ToastController } from '@ionic/angular';
import { FormsModule } from '@angular/forms';
import { Users } from 'src/app/services/users';
import { User } from 'src/app/core/models/user.model';

@Component({
  standalone: true,
  selector: 'app-user-edit',
  templateUrl: './user-edit.component.html',
  styleUrls: ['./user-edit.component.scss'],
  imports: [IonicModule, FormsModule],
})
export class UserEditComponent {

  @Input() user!: User;  // recebe o usuário a ser editado

  loading = false;

  constructor(
    private modalCtrl: ModalController,
    private toastCtrl: ToastController,
    private usersService: Users
  ) {}

  async save() {
    if (!this.user.name){
      await this.showToast('O nome não pode estar vazio', 'danger');
      return;
    } 

    this.loading = true;

    const dto = { name: this.user.name };

    this.usersService.update(this.user.id, dto).subscribe({
      next: async () => {
        this.loading = false;
        await this.showToast('Usuário atualizado com sucesso!');
        this.modalCtrl.dismiss({ updated: true });
      },
      error: async () => {
        this.loading = false;
        await this.showToast('Erro ao atualizar usuário', 'danger');
      }
    });
  }

  close() {
    this.modalCtrl.dismiss();
  }

  private async showToast(message: string, color: 'success' | 'danger' = 'success') {
    const toast = await this.toastCtrl.create({
      message,
      duration: 2000,
      color
    });
    toast.present();
  }
}
