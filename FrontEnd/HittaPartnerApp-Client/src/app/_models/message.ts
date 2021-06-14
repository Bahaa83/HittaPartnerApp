export interface Message {
  id:number;
  senderID:string;
  senderKnownAs:string;
  senderPhotoUrl:string;
  recipientID:string;
  recipientKnwonAs:string;
  recipientPhotoUrl:string;
  content:string;
  isRead:boolean;
  dateRead:Date;
  messageSent:Date;
  

}
