import { Component } from '@angular/core';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-settings-window',
  templateUrl: './settings-window.component.html',
  styleUrls: ['./settings-window.component.css']
})
export class SettingsWindowComponent {

  public showSavedToast: boolean = false;


  constructor(public authService: AuthService){}

  public saveSettings() {
    this.showSavedToast = true;
    setTimeout(() =>{
      this.showSavedToast = false;
    }, 2500)

  }

}
