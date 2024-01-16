import { Component, Input, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PhotoGalleryModule } from '@twogate/ngx-photo-gallery';
import { Book } from '../../../models/book-aggregate/book-models/book';
import { MdbDropdownModule } from 'mdb-angular-ui-kit/dropdown';
import { BookDetails } from '../../../models/book-aggregate/book-models/bookDetails';
import { BookService } from '../../../services/book.service';
import { ActivatedRoute } from '@angular/router';
import {
  NgxGalleryAnimation,
  NgxGalleryImage,
  NgxGalleryModule,
  NgxGalleryOptions,
} from '@kolkov/ngx-gallery';

@Component({
  selector: 'app-book-details',
  standalone: true,
  imports: [CommonModule, MdbDropdownModule, NgxGalleryModule],
  templateUrl: './book-details.component.html',
  styleUrls: ['./book-details.component.scss'],
})
export class BookDetailsComponent implements OnInit {
  book: BookDetails | null = null;

  galleryOptions: NgxGalleryOptions[] = [];
  galleryImages: NgxGalleryImage[] = [];

  constructor(
    public bookService: BookService,
    private route: ActivatedRoute,
  ) {}

  ngOnInit(): void {
    this.loadBook();

    this.galleryOptions = [
      {
        width: '80%',
        height: '640px',
        imagePercent: 100,
        thumbnailsColumns: 4,
        imageAnimation: NgxGalleryAnimation.Slide,
        preview: true,
      },
    ];
  }

  loadBook() {
    let id = Number(this.route.snapshot.paramMap.get('id'));
    this.bookService.getBookById(id).subscribe({
      next: (data) => {
        this.book = data;
        this.galleryImages = this.getGalleryImages();
      },
    });
  }

  getGalleryImages() {
    if (!this.book) return [];
    const imageUrls = [];

    for (const picture of this.book.pictures) {
      if (picture.isMain) {
        imageUrls.push({
          small: picture.url,
          medium: picture.url,
          big: picture.url,
        });
      }
    }

    for (const picture of this.book.pictures) {
      if (!picture.isMain) {
        imageUrls.push({
          small: picture.url,
          medium: picture.url,
          big: picture.url,
        });
      }
    }

    if (!this.book.pictures.length) {
      imageUrls.push({
        small:
          'https://images.unsplash.com/photo-1623697899811-f2403f50685e?ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MjZ8fGJvb2slMjBwbGFjZWhvbGRlcnxlbnwwfHwwfHx8MA%3D%3D&auto=format&fit=crop&w=500&q=60',
        medium:
          'https://images.unsplash.com/photo-1623697899811-f2403f50685e?ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MjZ8fGJvb2slMjBwbGFjZWhvbGRlcnxlbnwwfHwwfHx8MA%3D%3D&auto=format&fit=crop&w=500&q=60',
        big: 'https://images.unsplash.com/photo-1623697899811-f2403f50685e?ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MjZ8fGJvb2slMjBwbGFjZWhvbGRlcnxlbnwwfHwwfHx8MA%3D%3D&auto=format&fit=crop&w=500&q=60',
      });
    }
    console.log(imageUrls);
    return imageUrls;
  }

  getPaperbackPrice() {
    if (!this.book) return;
    for (let price of this.book.priceList) {
      if (price.measureOption === 'paperback') {
        return price.unitPrice;
      }
    }
    return;
  }

  getHardcoverPrice() {
    if (!this.book) return;
    for (let price of this.book.priceList) {
      if (price.measureOption === 'hardcover') {
        return price.unitPrice;
      }
    }
    return;
  }
}
