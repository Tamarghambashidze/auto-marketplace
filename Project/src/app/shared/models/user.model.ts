export class BaseUser {
    public profileImgUrl?: string;
    public firstName?: string;
    public lastName?: string;
    public email?: string;
    public userDetails?: UserDetails;
}

export class User extends BaseUser{
    public id!:number
}

export class CreateUser extends BaseUser{
    public passwordHash?: string
}

export class UserDetails {
    public address?: string;
    public phoneNumber?: string; 
    public dateOfBirth?: Date;
    public gender?: string
}
  