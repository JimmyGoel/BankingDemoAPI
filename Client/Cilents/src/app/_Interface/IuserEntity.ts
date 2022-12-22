import { Photo } from "./Iphoto";

export interface IUserEntity {
    Id: number;
    userName: string;
    Age: number;
    KnownUs?: any;
    Created: Date;
    LastActive: Date;
    Gender: string;
    LookingFor: string;
    Introduction: string;
    Intrested?: any;
    city: string;
    country: string;
    photos: Photo[];
    PhotoUrl: string;
}