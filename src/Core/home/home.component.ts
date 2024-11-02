import { Component, OnInit  } from '@angular/core';
import { ActivatedRoute } from '@angular/router';



@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent implements OnInit {

  successMessage: string | null = null;

  constructor(private route: ActivatedRoute) {}
  ngOnInit(): void {
    this.route.queryParams.subscribe(params => {
      this.successMessage = params['message'] || null;
    });
  }

   property='Roshan';
  dynamicColors: string[] = ['#ff0000', '#00ff00', '#0000ff'];
  addColor() {
    this.dynamicColors.push('#000000');  
   


  }








}


