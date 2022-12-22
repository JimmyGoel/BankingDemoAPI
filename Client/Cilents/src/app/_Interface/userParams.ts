import { IUserEntity } from "./IuserEntity";
import { IUserDetail } from "./user";

export class UserParams {
    gender: string;
    minAge = 18;
    maxAge = 99;
    pageNumber = 1;
    pageSize = 2;
    orderBy="lastActive"
    constructor(user: IUserDetail) {
        debugger;
        this.gender = user.gender === 'male' ? 'Female' : 'Male';
    }
}