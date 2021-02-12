import { BatchNumber, StatusEnum } from ".";


export interface Batch {
    id: number;
    count: number;
    status: StatusEnum;
    total: number;
    numbers: Array<BatchNumber>;
  }
  