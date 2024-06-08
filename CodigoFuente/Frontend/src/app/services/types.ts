export interface LoginModel {
  token: string;
}

export interface UserModel {
  name: string;
  email: string;
  role: string;
}

export interface InvitationModel {
  name: string;
  email: string;
  role: number;
  expirationDate: string;
}

export interface EditInvitationModel {
  id: number;
  expirationDate: string;
}

export interface TicketsByCategories {
  tickets: Object[];
}

export interface Categories {
  categories: Object[];
}
