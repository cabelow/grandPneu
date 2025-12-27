import { Component, Input } from '@angular/core';
import { IonicModule, ModalController } from '@ionic/angular';
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
    private usersService: Users
  ) {}

  save() {
    if (!this.user.name || !this.user.email) return;

    this.loading = true;

    this.usersService.update(this.user).subscribe({
      next: () => {
        this.loading = false;
        this.modalCtrl.dismiss({ updated: true });
      },
      error: () => {
        this.loading = false;
        alert('Erro ao atualizar usuário');
      }
    });
  }

  close() {
    this.modalCtrl.dismiss();
  }
}
