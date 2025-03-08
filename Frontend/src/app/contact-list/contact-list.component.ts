import { Component, OnInit } from '@angular/core';
import { ContactService } from '../contact.service';
import { IContact, IPagedResponse } from '../contact.model';


@Component({
  selector: 'app-contact-list',
  templateUrl: './contact-list.component.html',
  styleUrls: ['./contact-list.component.css'],
})
export class ContactListComponent implements OnInit {
  contacts: IPagedResponse<IContact>= {
    data: [],
    metadata: {
      currentPage: 1,
      pageSize: 5,
      totalItems: 0,
    },
  };
  newContact: IContact = {
    id: 0,
    name: '',
    email: '',
    phoneNumber: '',
  };
  search : string = '';
  loading: boolean = false;

  constructor(private contactService: ContactService) {}

  ngOnInit(): void {
    this.loadContacts();
  }

  loadContacts(search:string = '', page:number = 1, pageSize:number = 10):void{
    this.loading = true;
    this.contactService.getContacts(search, page, pageSize).subscribe((data) => {
      this.contacts = data;
      this.loading = false;
    });
  }

  createContact():void{
    this.contactService.createContact(this.newContact).subscribe((createdContact)=>{
      this.newContact = {
        id: 0,
        name: '',
        email: '',
        phoneNumber: '',
      };

      this.contacts.data.unshift(createdContact);
      this.contacts.metadata.totalItems++;
      if(this.contacts.data.length > this.contacts.metadata.pageSize){
        this.contacts.data.pop();
      }
    })
  }

  deleteContact(id: number): void {
    this.contactService.deleteContact(id).subscribe(() => {
      this.loadContacts();
    });
  }

  searchContacts():void{
    this.loadContacts(this.search);
  }

  getTotalPages:()=>number = ()=>{
    return Math.ceil(this.contacts.metadata.totalItems / this.contacts.metadata.pageSize);
  }

  hasNextPage:()=>boolean = ()=>{
    return this.contacts.metadata.currentPage < this.getTotalPages();
  }

  hasPreviousPage:()=>boolean = ()=>{
    return this.contacts.metadata.currentPage > 1;
  }

  loadNextPage():void{
    this.loadContacts(this.search, this.contacts.metadata.currentPage + 1);
  }

  loadPreviousPage():void{
    this.loadContacts(this.search, this.contacts.metadata.currentPage - 1);
  }
}
