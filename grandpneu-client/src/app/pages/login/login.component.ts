import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { IonicModule, ToastController } from '@ionic/angular';
import { AuthService } from 'src/app/services/auth';

@Component({
  standalone: true,
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
  imports: [IonicModule, FormsModule],
})
export class LoginComponent implements OnInit {

  email = '';
  password = '';
  loading = false;

  constructor(
    private authService: AuthService,
    private toastCtrl: ToastController,
    private router: Router,
    private routerLink: Router
  ) {}

  ngOnInit() {}

  login() {
    if (!this.email || !this.password) {
      this.showToast('Informe email e senha');
      return;
    }

    this.loading = true;

    this.authService.login({
      email: this.email,
      password: this.password,
    }).subscribe({
      next: () => {
        this.loading = false;
        this.showToast('Login realizado com sucesso');
        this.router.navigate(['/home']);
      },
      error: () => {
        this.loading = false;
        this.showToast('Email ou senha inválidos');
      }
    });
  }

  private async showToast(message: string) {
    const toast = await this.toastCtrl.create({
      message,
      duration: 2000,
      position: 'bottom',
    });
    toast.present();
  }
}
