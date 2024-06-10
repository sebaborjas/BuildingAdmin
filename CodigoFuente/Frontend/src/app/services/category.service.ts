import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { CategoriesEndpoint } from '../../networking/endpoints';
import { Observable } from 'rxjs';
import { CategoryModel, CreateCategoryModel, } from './types';
import { environment } from '../../environments/environment.development';

const BASE_URL = environment.API_URL;

@Injectable({
  providedIn: 'root',
})
export class CategoryService {
  constructor(private _httpClient: HttpClient) { }

  getCategories(id?: string): Observable<CategoryModel[]> {
    let params = new HttpParams()

    if (id) {
      params = params.set('id', id);
    }

    return this._httpClient.get<CategoryModel[]>(
      `${BASE_URL}${CategoriesEndpoint.CATEGORIES}`,
      { params: params }
    );
  }

  createCategory(name: string, parentCategoryId?: number): Observable<CreateCategoryModel> {
    const body = {
      name: name,
      parentCategoryId: parentCategoryId
    };
    return this._httpClient.post<CreateCategoryModel>(
      `${BASE_URL}${CategoriesEndpoint.CATEGORIES}`,
      body
    );
  }
}
