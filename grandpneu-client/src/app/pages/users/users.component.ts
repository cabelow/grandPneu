import { Component, OnInit } from '@angular/core';
import { IonicModule, ModalController } from '@ionic/angular';
import { UserEditComponent } from './user-edit/user-edit.component';
import { Users } from 'src/app/services/users';
import { AuthStorage } from 'src/app/services/auth-storage';
import { User } from 'src/app/core/models/user.model';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  standalone: true,
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.scss'],
  imports: [
    CommonModule,
    FormsModule,
    IonicModule,
    UserEditComponent
  ],
})
export class UsersComponent implements OnInit {
  users: User[] = [];
  loading = false;
  currentUserRole = 0;

  constructor(
    private usersService: Users,
    private modalCtrl: ModalController,
    private authStorage: AuthStorage
  ) {}

  loadUsers() {
    this.loading = true;
    console.log('Loading users...');
    this.usersService.getAll().subscribe({
      next: data => { this.users = data; this.loading = false; },
      error: () => this.loading = false
    });
  }

  async editUser(user: User) {
    if (this.currentUserRole !== 1) return;

    const modal = await this.modalCtrl.create({
      component: UserEditComponent,
      componentProps: { user }
    });

    modal.onDidDismiss().then(res => {
      if (res.data?.updated) this.loadUsers();
    });

    return modal.present();
  }

  private getRoleFromToken(): number {
    const token = this.authStorage.getToken();
    if (!token) return 0;
    try {
      const payload = JSON.parse(atob(token.split('.')[1]));
      console.log('Decoded token payload:', payload);
      return payload.role ?? 0;
    } catch {
      return 0;
    }
  }

  ngOnInit() {
    console.log('UsersComponent initialized');
    this.loadUsers();
    this.currentUserRole = this.getRoleFromToken();
    console.log('Current user role:', this.currentUserRole);
  }
}
