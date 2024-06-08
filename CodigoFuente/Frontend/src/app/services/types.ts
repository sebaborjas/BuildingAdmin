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

export interface Category {
  name: string;
  id: number;
  relatedTo: string | null;
}

export interface AssignedTo {
  lastName: string;
  buildings: any | null;
  id: number;
  name: string;
  email: string;
  password: string;
}

export interface TicketModel {
  id: number;
  description: string;
  creationDate: string;
  apartment: string | null;
  totalCost: number;
  createdBy: string | null;
  category: Category;
  status: string;
  attentionDate: string;
  closingDate: string;
  assignedTo: AssignedTo;
}
