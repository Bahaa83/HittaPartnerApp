export interface User {
    id:string;
    userName:string;
    knownAs:string;
    age:number;
    gender:string;
    created:Date;
    lastActiv:Date;
    photoUrl:string;
    city:string;
    country:string;
    interests?:string;
    introduction?:string;
    lokingFor?:string;
    photos?:Photo[];

}
