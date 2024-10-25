import { CommonModule } from '@angular/common';
import { Component, OnInit, QueryList, ViewChild, ViewChildren } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { CreateTripModel } from '../../models/create-trip.model';
import { HttpService } from '../../services/http.service';
import { TripContentComponent } from "../trip-content/trip-content.component";
import { CreateTripContentModel } from '../../models/create-trip-content.model';
import { TripModel } from '../../models/trip.model';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [CommonModule, FormsModule, TripContentComponent],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent implements OnInit {

  constructor(
    private http:HttpService
  ){}

  tripModel:TripModel[]=[];

  @ViewChildren(TripContentComponent) tripContentComponents !: QueryList<TripContentComponent>;

  tripCounter:number=1;
  maxTripCounter:number=10;
  tripContents:number[]=[];

  createTripModel:CreateTripModel=new CreateTripModel();

  ngOnInit(): void {
    this.http.post<TripModel>("Trip/GetAll",{},res=>{
      this.tripModel=res.data;
    })
  }

  createTrip(form:NgForm){
    const allTripContents: CreateTripContentModel[] = this.tripContentComponents.map(tripContent => {
      return tripContent.getTripContentData();
    })

    this.createTripModel.tripContents = allTripContents;

    const formData = new FormData();

    formData.append("title",this.createTripModel.title);
    formData.append("description",this.createTripModel.description);
    formData.append("tags",this.createTripModel.tags);
    formData.append("image",this.createTripModel.image);

    this.createTripModel.tripContents.forEach((content, index) => {
      formData.append(`tripContents[${index}].Title`, content.title);
      formData.append(`tripContents[${index}].Description`, content.description);
      formData.append(`tripContents[${index}].Image`, content.image);
    });

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

  GetFromTag(tagName:string){
    this.http.post<TripModel>("Trip/GetFromTag",{
      TagName:tagName},res=>{
      this.tripModel=res.data
    })
  }
}
