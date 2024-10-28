import { Component, Input } from '@angular/core';
import { CreateTripContentModel } from '../../models/create-trip-content.model';
import { FormsModule } from '@angular/forms';
import { UpdateTripContentModel } from '../../models/update-trip-content.model';
import { CommonModule } from '@angular/common';
import { contentsImage } from '../../constants';

@Component({
  selector: 'app-trip-content',
  standalone:true,
  imports:[FormsModule,CommonModule],
  templateUrl: './trip-content.component.html',
  styleUrl: './trip-content.component.css'
})
export class TripContentComponent {
  @Input() tripCounter!:number;
  id:string="";
  title:string="";
  description:string="";
  image:any=null;
  imageUrl:string="";
  imagePreview:string | ArrayBuffer | null = null;

  contentsImage:string = contentsImage;

  selectImage(event:any){
    const file=event.target.files[0];
    if(file){
      this.image=file;
      this.previewImage(file);
    }
    else{
      this.imagePreview=contentsImage+this.imageUrl;
    }
  }

  previewImage(file:File){
    const reader = new FileReader();
    reader.onload = () =>{
      this.imagePreview = reader.result;
    };
    reader.readAsDataURL(file);
  }

  setValues(id:string,title:string,description:string,imageUrl:string){
    this.title=title;
    this.description=description;
    this.id=id,
    this.imageUrl=imageUrl;
    this.imagePreview=contentsImage+imageUrl
  }

  getTripContentData():CreateTripContentModel{
    return{
      title:this.title,
      description:this.description,
      image:this.image,
    }
  }

  getUpdateTripContentData():UpdateTripContentModel{
    return{
      id:this.id,
      title:this.title,
      description:this.description,
      image:this.image,
      imageUrl:this.imageUrl
    }
  }
}
