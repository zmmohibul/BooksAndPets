export interface Category {
  id: number;
  name: string;
  subCategories?: Category[];
  parentId?: number | null;
}
