import { Component } from '@angular/core';

@Component({
  selector: 'app-settings-window',
  templateUrl: './settings-window.component.html',
  styleUrls: ['./settings-window.component.css']
})
export class SettingsWindowComponent {

  public showSavedToast: boolean = false;

  constructor(){}

  public saveSettings() {
    this.showSavedToast = true;
    setTimeout(() =>{
      this.showSavedToast = false;
    }, 2000)

  }

}
