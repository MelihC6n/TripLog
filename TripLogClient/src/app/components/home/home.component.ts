import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { TripModel } from '../../models/trip.model';
import { CreateTripModel } from '../../models/create-trip.model';
import { HttpService } from '../../services/http.service';
import { TripContentComponent } from "../trip-content/trip-content.component";

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [CommonModule, FormsModule, TripContentComponent],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent {

  constructor(
    private http:HttpService
  ){}

  tripCounter:number=1;
  maxTripCounter:number=10;
  tripContents:number[]=[];

  createTripModel:CreateTripModel=new CreateTripModel();

  createTrip(form:NgForm){
    const formData = new FormData();
    formData.append("title",this.createTripModel.title);
    formData.append("description",this.createTripModel.description);
    formData.append("tags",this.createTripModel.tags);
    formData.append("image",this.createTripModel.image);

    this.http.post("Trip/Create",formData,res=>{
      console.log(res.data);
    })
    console.log(this.createTripModel);
  }

  selectImage(event:any){
    const file=event.target.files[0];
    if(file){
      this.createTripModel.image=file;
    }
  }

  addTripPart(){
    if(this.tripContents.length<this.tripCounter){
      this.tripContents.push(this.tripCounter);
      this.tripCounter++;
      console.log(this.tripContents);
    }
  }
}
