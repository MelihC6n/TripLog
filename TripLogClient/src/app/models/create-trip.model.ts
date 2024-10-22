import { TagModel } from "./tag.model";
import { TripContent } from "./trip-content.model";

export class CreateTripModel{
    id:string="";
    title:string="";
    description:string="";
    image:any=null;
    tags:string="";
    tripContents:TripContent[]=[];
}