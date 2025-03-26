import {Component, inject} from '@angular/core';
import {ThemeComponent} from '../theme/theme.component';
import {Router, RouterLink} from '@angular/router';
import {AuthService} from '../../data/auth.service';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [ThemeComponent],
  templateUrl: './header.component.html',
  styleUrl: './header.component.scss'
})
export class HeaderComponent {
  router = inject(Router);
  authService=inject(AuthService);
  logout(){
    this.authService.logout();
    window.location.reload();
  }
}
