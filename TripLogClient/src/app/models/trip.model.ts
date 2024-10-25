import { TagModel } from "./tag.model";
import { TripContent } from "./trip-content.model";

export class TripModel{
    id:string="";
    title:string="";
    description:string="";
    imageUrl:string="";
    createdDate:string="";
    tags:TagModel[]=[];
    tripContents:TripContent[]=[];
}