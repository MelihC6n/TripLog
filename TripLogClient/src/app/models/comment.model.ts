import { UserModel } from "./user.model";

export class CommentModel{
    id:string="";
    text:string="";
    createdAt:string="";
    appUser:UserModel=new UserModel;
}