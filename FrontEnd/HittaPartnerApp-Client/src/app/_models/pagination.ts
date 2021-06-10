export interface Pagination {
    cueerntPage:number;
    itemsPerPage:number;
    totalItems:number;
    totalPages:number;
}
export class PaginationResult<T>{
    result!: T;
    pagination!: Pagination; 
}
