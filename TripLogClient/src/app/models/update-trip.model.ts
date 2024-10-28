import { TagModel } from "./tag.model";
import { UpdateTripContentModel } from "./update-trip-content.model";


export class UpdateTripModel{
    id:string="";
    title:string="";
    description:string="";
    imageUrl:string="";
    image:any=null;
    createdDate:string="";
    tags:string="";
    tripContents:UpdateTripContentModel[]=[];
}