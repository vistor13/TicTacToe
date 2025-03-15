import { Component } from '@angular/core';
import {ThemeService} from '../../data/theme.service';
import {NgIf} from '@angular/common';

@Component({
  selector: 'app-theme',
  imports: [
    NgIf
  ],
  templateUrl: './theme.component.html',
  styleUrl: './theme.component.scss'
})
export class ThemeComponent {
  isDarkMode: boolean = false;

  constructor(private themeService: ThemeService) {
    this.isDarkMode = this.themeService.getTheme() == 'dark' ;
  }

  toggleTheme() {
    this.isDarkMode = !this.isDarkMode;
    const newTheme = this.isDarkMode ? 'dark' : 'light';
    this.themeService.setTheme(newTheme);
  }
}
