import { Injectable } from '@angular/core';
import { Headers, Http, Response } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';
import { CategoryService } from 'src/app/webapi/services';

@Injectable()
export class CategoryServiceApp {

    constructor(private apiCategoryService: CategoryService) { }

    getCategoryList(): Observable<any> {
        return this.apiCategoryService.ApiCategoryCategoriesGet().map(x => (x));
    }
}
