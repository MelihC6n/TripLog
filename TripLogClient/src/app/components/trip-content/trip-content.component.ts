import { Component, Input } from '@angular/core';
import { CreateTripContentModel } from '../../models/create-trip-content.model';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-trip-content',
  standalone:true,
  imports:[FormsModule],
  templateUrl: './trip-content.component.html',
  styleUrl: './trip-content.component.css'
})
export class TripContentComponent {
  @Input() tripCounter!:number;
  title:string="";
  description:string="";
  image:any=null;

  selectImage(event:any){
    const file=event.target.files[0];
    if(file){
      this.image=file;
    }
  }

  setValues(title:string,description:string){
    this.title=title;
    this.description=description;
  }

  getTripContentData():CreateTripContentModel{
    return{
      title:this.title,
      description:this.description,
      image:this.image
    }
  }
}
