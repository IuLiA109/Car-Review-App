import { Component, OnInit, inject } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import {HttpClient, HttpClientModule } from '@angular/common/http';
import { CommonModule } from '@angular/common'; 
@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, HttpClientModule,CommonModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit{
  title = 'Car-App';
  httpClient = inject(HttpClient);
  cars:any = [];
  fetchCar():void{
      this.httpClient.get('https://localhost:7173/api/Car').subscribe((data:any)=>{
        this.cars = data;
        console.log(this.cars);
      });
  }
  fetchReviews(carId: number): void {
    this.httpClient.get(`https://localhost:7173/api/Review/car/${carId}`).subscribe((reviews: any) => {
      const car = this.cars.find((c: any) => c.id === carId);
      if (car) {
        car.reviews = reviews;
      }
    });
  }
  ngOnInit(): void {
    this.fetchCar();
  }
}
