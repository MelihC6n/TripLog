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
import { CreateUserModel } from '../../models/create-user.model';
import { LoginModel } from '../../models/login.model';
import { UserModel } from '../../models/user.model';
import { AuthService } from '../../services/auth.service';
import { SendCommentModel } from '../../models/send-comment.model';
import { CommentModel } from '../../models/comment.model';
import moment from 'moment';
import 'moment/locale/tr';

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
    private swal:SwalService,
    private auth:AuthService
  ){
    moment.locale('tr');
  }
  placeholder:number[]=[1,2,3];

  tripModel:TripModel[]=[];

  @ViewChildren(TripContentComponent) tripContentComponents !: QueryList<TripContentComponent>;
  @ViewChildren(TripContentComponent) updateTripContentComponents !: QueryList<TripContentComponent>;


  @ViewChild("modelCloseBtn") modelCloseBtn : ElementRef<HTMLButtonElement> | undefined;
  @ViewChild("updateModelCloseBtn") updateModelCloseBtn : ElementRef<HTMLButtonElement> | undefined;
  @ViewChild("loginModalCloseBtn") loginModalCloseBtn:ElementRef<HTMLButtonElement> | undefined;
  @ViewChild("loginModalBtn") loginModalBtn:ElementRef<HTMLButtonElement> | undefined;

  tripsImage:string=tripsImage;
  contentsImage:string=contentsImage;

  tripCounter:number=1;
  maxTripCounter:number=10;
  tripContents:number[]=[];
  
  updateTripContents:number[]=[];
  result:number=0;


  createTripModel:CreateTripModel=new CreateTripModel();
  updateTripModel:UpdateTripModel=new UpdateTripModel();

  createUserModel:CreateUserModel=new CreateUserModel();
  loginModel:LoginModel=new LoginModel();
  activeUser:null|UserModel=null;

  imagePreview:string | ArrayBuffer | null = null;
  updateImagePreview:string | ArrayBuffer | null = null;

  showOrHidePassword(event: Event){
    const input = event.target as HTMLElement;
    const password = input.previousElementSibling as HTMLInputElement;
    if(password===undefined) return;

    if(password.type==="password"){
      password.type="text";
    }
    else{
      password.type="password";
    }
  }

  changeText(event: Event){
    const label = event.target as HTMLElement;
    if(label===undefined) return;

    if(label.innerText==="Yazar Olmak İstiyorum!"){
      label.innerText="Yazar Olmak İstemiyorum...";
      this.createUserModel.isAuthor=false;
    }
    else{
      label.innerText="Yazar Olmak İstiyorum!";
      this.createUserModel.isAuthor=true;
    }
  }

  ngOnInit(): void {
    this.singHello();
    this.getAll();
  }

  getAll(){
    this.http.post<TripModel>("Trip/GetAll",{},res=>{
      this.tripModel=res.data;
      console.log(res.data);
      console.log(this.tripModel);
    })
    console.log(this.tripModel);
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
    formData.append("appUserId",this.activeUser!.id);

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
      debugger
      if(file.type.includes('image'))
      {
        this.createTripModel.image=file;
        this.previewImage(file,true);
      }
      else{
        this.swal.callToast("Lütfen sadece resim formatında giriş yapınız",'error')
        event.target.value='';
      }
    }
    else{
        this.imagePreview="";
    }
  }

  showContentOfTrip(tripId:string){
    this.http.post('Trip/GetContentOfTrip',{tripId},res=>{
      if(res.data){
        console.log(res.data);
        const trip = this.tripModel.find(t=>t.id == tripId);
        trip!.tripContents=[...res.data.tripContents];
        trip!.comments=[...res.data.comments];
        console.log(this.tripModel);
      }
    })
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

  deleteTripPart(index:number){
    this.tripContents.splice(index,1);
    this.tripCounter--;
    this.createTripModel.tripContents?.splice(index,1);
  }

  deleteUpdateTripPart(index:number){
    this.updateTripContents.splice(index,1);
    this.tripCounter--;
    this.updateTripModel.tripContents?.splice(index,1);
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
    this.tripCounter=1;
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

  deleteTrip(id:string,title:string){
    this.swal.callSwal("Gezi Silme Onayı!",title + " isimli gezi silinecek emin misiniz?",() => {
      this.http.post("Trip/Delete",{
        id
      },res=>{
        this.swal.callToast(res.data,'info');
        this.getAll();
      });
    });
  }

  createUser(registerForm:NgForm){
    this.http.post("User/Create",this.createUserModel,res=>{
      if(res.data!=null)
      {
        this.swal.callToast(res.data,"success");
      }
    })
  }

  signIn(loginForm:NgForm){
    this.http.post("Auth/Login",this.loginModel,res=>{
    localStorage.setItem("token",res.data?.token); 
    this.loginModalCloseBtn?.nativeElement.click();
    this.singHello();
    })
  }

  singHello(){
    this.activeUser = this.auth.isUserActive();
    if(this.activeUser){
      this.swal.callToast("Hoş geldin "+ this.activeUser?.userName+"!",'info');
    }
  }

  singOut(){
    localStorage.removeItem("token");
    this.activeUser = this.auth.isUserActive();
  }

  commentAccount(){
    if(!this.activeUser){
      this.loginModalBtn?.nativeElement.click();
      this.swal.callToast("Yorum yazmak için giriş yapmalısınız!",'warning');
    }
  }

sendCommentModel:SendCommentModel= new SendCommentModel;
getRelativeTime(date: string): string {
  return moment(date).fromNow();
}


  sendComment(form:NgForm,tripId:string){
    if(!this.activeUser){
      this.commentAccount()
    }
    else{
      if(form.valid){
        this.sendCommentModel.appUserId=this.activeUser.id;
        this.sendCommentModel.tripId=tripId;
        this.http.post<string>("Comment/Send",this.sendCommentModel,res=>{
          if(res.data){
            this.sendCommentModel=new SendCommentModel;
            this.swal.callToast(res.data);
            this.http.post<CommentModel[]>("Comment/GetTripComments",{tripId},comment=>{
              if(comment.data){
                const trip = this.tripModel.find(t=>t.id==tripId);
                trip!.comments=[...comment.data];
              }
            })
          }
        });
      }
    }
  }
}
