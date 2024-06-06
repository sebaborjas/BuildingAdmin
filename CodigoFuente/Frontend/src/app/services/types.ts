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
  role: string;
  expirationDate: string;
}
