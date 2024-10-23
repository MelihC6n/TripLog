import { TripContent } from "./trip-content.model";

export class CreateTripModel{
    title:string="";
    description:string="";
    image:any=null;
    tags:string="";
    tripContents:TripContent[]=[];
}