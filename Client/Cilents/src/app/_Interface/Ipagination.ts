export interface IPagination {
    CurrentPage: number,
    PageSize: number,
    TotalCount: number,
    TotalPage: number,
    TotalItems:number,
    ItemPerPage:number
}

export class PaginatedResult<T>{
    result: T ;
    pagination: IPagination;
}