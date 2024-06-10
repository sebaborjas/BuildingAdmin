export enum LoginEndpoint {
  LOGIN = '/api/v2/login',
}

export enum UserEndpoint {
  USER = '/api/v2/users',
  MANAGER = '/api/v2/users/manager',
  COMPANYADMINISTRATOR = '/api/v2/users/company-administrator',
  ADMINISTRATOR = '/api/v2/users/administrator',
  MAINTENANCEOPERATOR = '/api/v2/users/maintenance-operator',
}

export enum InvitationEndpoint {
  INVITATIONS = '/api/v2/invitations',
  ACCEPT = '/api/v2/invitations/accept',
  REJECT = '/api/v2/invitations/reject',
}

export enum BuildingEndpoint {
  BUILDINGS = '/api/v2/buildings',
}

export enum ConstructionCompany {
  CONSTRUCTION_COMPANIES = '/api/v2/constructionCompanies',
}

export enum ImporterEndpoint{
  IMPORTERS = '/api/v2/importers',
}

export enum TicketEndpoint {
  TICKETS = '/api/v2/tickets',
}

export enum ReportEndpoint {
  REPORTS = '/api/v2/reports',
}

export enum CategoriesEndpoint {
  CATEGORIES = '/api/v2/categories',
}