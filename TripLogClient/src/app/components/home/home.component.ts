import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { TripModel } from '../../models/trip.model';
import { CreateTripModel } from '../../models/create-trip.model';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [CommonModule,FormsModule],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent {
  createTripModel:CreateTripModel=new CreateTripModel();

  createTrip(form:NgForm){
    console.log(this.createTripModel);
  }

  selectImage(event:any){
    const file=event.target.files[0];
    if(file){
      this.createTripModel.image=file;
    }
  }
}
