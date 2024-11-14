import { Component, OnInit  } from '@angular/core';




@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent {

  

  constructor() {}
  // ngOnInit(): void {
  //   this.route.queryParams.subscribe(params => {
  //     this.successMessage = params['message'] || null;

  //     if(this.successMessage){
  //        setTimeout(()=>
  //        {
  //         this.successMessage= null;
  //        }, 3000
  //        );

  //     }
  //   });
  // }

   property='Roshan';
  dynamicColors: string[] = ['#ff0000', '#00ff00', '#0000ff'];
  addColor() {
    this.dynamicColors.push('#000000');  
   


  }








}


