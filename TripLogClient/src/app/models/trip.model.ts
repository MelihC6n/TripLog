import { TagModel } from "./tag.model";
import { TripContent } from "./trip-content.model";
import { UserModel } from "./user.model";

export class TripModel{
    id:string="";
    title:string="";
    description:string="";
    imageUrl:string="";
    createdDate:string="";
    appUserId:string="";
    tags:TagModel[]=[];
    tripContents:TripContent[]=[];
    appUser:UserModel=new UserModel;
}