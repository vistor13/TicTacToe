import { Component } from '@angular/core';
import {ThemeComponent} from '../theme/theme.component';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [ThemeComponent],
  templateUrl: './header.component.html',
  styleUrl: './header.component.scss'
})
export class HeaderComponent {

}
