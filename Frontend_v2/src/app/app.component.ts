import { Component, OnInit } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ThumbnailsPosition } from 'ng-gallery';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'MenuGenius';
  loginModalOpen: boolean = false;
  registerModalOpen: boolean = false;
  
  
  constructor(
    private titleService: Title, 
    public modalService: NgbModal,
    ) {
  }

  ngOnInit(): void{
    this.titleService.setTitle('MenuGenius');
    //this.setDefaultFontSize();
  }

  openLoginModal(){
    this.loginModalOpen = true;
  }

  openRegisterModal(){
    this.registerModalOpen = true;
  }

  // @HostListener('window:resize', ['$event'])
  // onResize() {
  //   //this.setDefaultFontSize();
  //   //this.cdr.detectChanges();
  //   this.onResize();
  // }

  // setDefaultFontSize() {
  //   const width = window.innerWidth;
  //   const height = window.innerHeight;
  //   const fontSize = Math.min(width, height) * 0.04;
  //   document.body.style.fontSize = fontSize + 'px';
  // }

}
