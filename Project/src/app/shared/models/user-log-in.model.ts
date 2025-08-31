export class UserLogIn {
    public email?: string;
    public passwordHash!: string;
}

export class UserPasswordUpdate {
    public email?: string;
    public oldPassword?:string;
    public newPassword?: string;
}
