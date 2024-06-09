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

export interface CreateCategoryModel {
  name: string;
}

export interface BuildingModel {
  buildings: Object[];
}

export interface AdminModel {
  name: string;
  lastName: string;
  email: string;
  password: string;
}