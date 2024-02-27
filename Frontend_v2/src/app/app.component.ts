import { Component, HostListener, ChangeDetectorRef } from '@angular/core';
import { Title } from '@angular/platform-browser';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'MenuGenius';
  constructor(private titleService: Title, private cdr: ChangeDetectorRef) {}

  ngOnInit(): void{
    this.titleService.setTitle('MenuGenius');
    //this.setDefaultFontSize();
  }

  @HostListener('window:resize', ['$event'])
  onResize() {
    //this.setDefaultFontSize();
    //this.cdr.detectChanges();
    this.onResize();
  }

  // setDefaultFontSize() {
  //   const width = window.innerWidth;
  //   const height = window.innerHeight;
  //   const fontSize = Math.min(width, height) * 0.04;
  //   document.body.style.fontSize = fontSize + 'px';
  // }
}
