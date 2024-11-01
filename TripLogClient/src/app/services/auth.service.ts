import { Injectable } from '@angular/core';
import { UserModel } from '../models/user.model';
import { jwtDecode, JwtPayload } from 'jwt-decode';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  userModel:null|UserModel=null;

  constructor() { }

  isUserActive(){
    const token:string|null = localStorage.getItem("token");
    this.userModel=null;

    if(token){
      const decode:JwtPayload | any = jwtDecode(token);
      const exp = decode.exp;
      const now = new Date().getTime()/1000;

      if(now>exp){
        localStorage.removeItem("token");
        return this.userModel;
      }

      this.userModel=new UserModel();
      this.userModel.id=decode["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"];
      this.userModel.userName=decode["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"];
      this.userModel.email=decode["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress"];
      this.userModel.isAuthor=decode["IsAuthor"];
      return this.userModel;
    }

    return this.userModel;
  }
}
