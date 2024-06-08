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

export interface ConstructionCompanyModel{
  id: number;
  name: string;
}

export interface BuildingModel {
  id: number;
  name: string;
  address: string;
  location: string;
  managerName?: string;
}

export interface CreateApartmentModel {
  floor: number;
  doorNumber: number;
  ownerName: string;
  ownerLastName: string;
  ownerEmail: string;
  rooms: number;
  bathrooms: number;
  hasTerrace: boolean;
}

export interface CreateBuildingModel {
  name: string;
  address: string;
  location: string;
  expenses: number;
  apartments: CreateApartmentModel[];
  managerEmail: string;
}