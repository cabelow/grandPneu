import { Component } from '@angular/core';
import { IonicModule, ToastController } from '@ionic/angular';
import { FormsModule } from '@angular/forms';
import { Users } from 'src/app/services/users';
import { CommonModule } from '@angular/common';

@Component({
  standalone: true,
  selector: 'app-register-user',
  templateUrl: './register-user.component.html',
  styleUrls: ['./register-user.component.scss'],
  imports: [IonicModule, FormsModule, CommonModule],
})
export class RegisterUserComponent {
  name = '';
  email = '';
  role: number = 3;
  password = '';
  confirmPassword = '';
  loading = false;
  passwordMessage = 'A senha deve ter no mínimo 8 caracteres, 1 letra maiúscula e 1 número';
  passwordValid = false;

  constructor(
    private usersService: Users,
    private toastCtrl: ToastController
  ) { }

  checkPassword() {
    const regex = /^(?=.*[A-Z])(?=.*\d).{8,}$/;
    if (!regex.test(this.password)) {
      this.passwordMessage = 'A senha deve ter no mínimo 8 caracteres, 1 letra maiúscula e 1 número';
      this.passwordValid = false;
    } else if (this.password !== this.confirmPassword) {
      this.passwordMessage = 'As senhas não coincidem';
      this.passwordValid = false;
    } else {
      this.passwordMessage = 'Senhas OK';
      this.passwordValid = true;
    }
  }

  register() {
    if (!this.name || !this.email || !this.password) {
      this.showToast('Informe nome, email e senha', 'danger');
      return;
    }

    this.checkPassword();

    if (!this.passwordValid) return;

    this.loading = true;

    const user = { name: this.name, email: this.email, role: this.role, password: this.password };

    this.usersService.register(user).subscribe({
      next: async () => {
        this.loading = false;
        await this.showToast('Usuário registrado com sucesso!');
        this.resetForm();
      },
      error: async () => {
        this.loading = false;
        await this.showToast('Erro ao registrar usuário', 'danger');
      }
    });
  }

  private resetForm() {
    this.name = '';
    this.email = '';
    this.role = 3;
    this.password = '';
    this.confirmPassword = '';
    this.passwordMessage = 'A senha deve ter no mínimo 8 caracteres, 1 letra maiúscula e 1 número';
    this.passwordValid = false;
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
