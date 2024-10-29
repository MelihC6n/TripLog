import { CommonModule } from '@angular/common';
import { Component, ElementRef, OnInit, QueryList, ViewChild, ViewChildren } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { CreateTripModel } from '../../models/create-trip.model';
import { HttpService } from '../../services/http.service';
import { TripContentComponent } from "../trip-content/trip-content.component";
import { CreateTripContentModel } from '../../models/create-trip-content.model';
import { TripModel } from '../../models/trip.model';
import { SwalService } from '../../services/swal.service';
import { UpdateTripModel } from '../../models/update-trip.model';
import { UpdateTripContentModel } from '../../models/update-trip-content.model';
import { contentsImage, tripsImage } from '../../constants';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [CommonModule, FormsModule, TripContentComponent],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent implements OnInit {

  constructor(
    private http:HttpService,
    private swal:SwalService
  ){}

  tripModel:TripModel[]=[];

  @ViewChildren(TripContentComponent) tripContentComponents !: QueryList<TripContentComponent>;
  @ViewChildren(TripContentComponent) updateTripContentComponents !: QueryList<TripContentComponent>;


  @ViewChild("modelCloseBtn") modelCloseBtn : ElementRef<HTMLButtonElement> | undefined;
  @ViewChild("updateModelCloseBtn") updateModelCloseBtn : ElementRef<HTMLButtonElement> | undefined;

  tripsImage:string=tripsImage;
  contentsImage:string=contentsImage;

  tripCounter:number=1;
  maxTripCounter:number=10;
  tripContents:number[]=[];
  
  updateTripContents:number[]=[];

  createTripModel:CreateTripModel=new CreateTripModel();
  updateTripModel:UpdateTripModel=new UpdateTripModel();

  imagePreview:string | ArrayBuffer | null = null;
  updateImagePreview:string | ArrayBuffer | null = null;

  ngOnInit(): void {
    this.getAll();
  }

  getAll(){
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
      this.modelCloseBtn?.nativeElement.click();
      this.swal.callToast(this.createTripModel.title + " gezisi başarıyla eklendi!","success");
      this.getAll();
    })
  }

  selectImage(event:any){
    const file=event.target.files[0];
    if(file){
      this.createTripModel.image=file;
      this.previewImage(file,true);
    }
    else{
        this.imagePreview="";
    }
  }

  selectUpdateImage(event:any){
    const file=event.target.files[0];
    if(file){
      this.updateTripModel.image=file;
      this.previewImage(file,false);
    }
    else{
      if(this.updateTripModel.imageUrl)
      {
        this.updateImagePreview=tripsImage+this.updateTripModel.imageUrl;
      }
      else
      {
        this.updateImagePreview="";
      }
    }
  }

  previewImage(file:File,creteOrUpdate:boolean){
    const reader = new FileReader();
    reader.onload = () =>{
      if(creteOrUpdate)
      {
        this.imagePreview = reader.result;
      }
      else{
        this.updateImagePreview= reader.result;
      }
    };
    reader.readAsDataURL(file);
  }

  addTripPart(){
    if(this.tripContents.length<this.tripCounter){
      this.tripContents.push(this.tripCounter);
      this.tripCounter++;
    }
  }

  addUpdateTripPart(){
    if(this.updateTripContents.length<this.tripCounter){
      this.updateTripContents.push(this.tripCounter);
      this.tripCounter++;    }
  }

  GetFromTag(tagName:string){
    this.http.post<TripModel>("Trip/GetFromTag",{
      TagName:tagName},res=>{
      this.tripModel=res.data
    })
  }

  cancelTrip(){
    this.createTripModel=new CreateTripModel;
    this.updateTripModel=new UpdateTripModel;
    this.imagePreview=null;
    this.tripContents= [];
    this.updateTripContents=[]
  }

  openUpdateModal(trip:TripModel){
    this.updateTripModel.id=trip.id;
    this.updateTripModel.title=trip.title;
    this.updateTripModel.description=trip.description;
    this.updateTripModel.tags="";
    this.updateTripModel.tags= trip.tags.map(tag=>tag.name).join(" ");
    this.updateTripModel.imageUrl=trip.imageUrl
    this.updateImagePreview=tripsImage+this.updateTripModel.imageUrl;

    this.updateTripContents=[];
    this.tripCounter=1;

    trip.tripContents.forEach((content,index) => {
      this.addUpdateTripPart();
      setTimeout(()=>{
        const currentComponent = this.updateTripContentComponents.toArray()[index];
        if(currentComponent){
          currentComponent.setValues(content.id,content.title,content.description,content.imageUrl)
        }
      });
    });
  }

  updateTrip(form:NgForm){
    const allTripContents: UpdateTripContentModel[] = this.tripContentComponents.map(tripContent => {
      return tripContent.getUpdateTripContentData();
    });

    this.updateTripModel.tripContents=allTripContents;

    const formData = new FormData();

    formData.append("id",this.updateTripModel.id);
    formData.append("title",this.updateTripModel.title);
    formData.append("description",this.updateTripModel.description);
    formData.append("tags",this.updateTripModel.tags);
    formData.append("image",this.updateTripModel.image);

    this.updateTripModel.tripContents.forEach((content, index) => {
      formData.append(`tripContents[${index}].id`, content.id);
      formData.append(`tripContents[${index}].title`, content.title);
      formData.append(`tripContents[${index}].description`, content.description);
      formData.append(`tripContents[${index}].image`, content.image);
    });

    console.log(this.updateTripModel);

    this.http.post("Trip/Update",formData,res=>{
      this.updateModelCloseBtn?.nativeElement.click();
      this.swal.callToast(this.updateTripModel.title + " gezisi başarıyla güncellendi!","success");
      this.getAll();
    })
  }
}
