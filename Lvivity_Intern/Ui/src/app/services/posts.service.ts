import { Injectable } from '@angular/core';
import { Subject, throwError } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Post } from '../models/post.interface';
import { ShowToastrService } from './show-toastr.service';
import { map } from 'rxjs/operators';

const API_URL = 'http://localhost:5000/api/';

@Injectable({
  providedIn: 'root'
})
export class PostsService {
  private posts : Post[] = [];
  private postsUpdated = new Subject<Post[]>();

  constructor (
    private http: HttpClient,
    private toastr: ShowToastrService
  ) {}

  getPosts(perPage: number, currentPage: number): void {
    this.http.post<{
      result: {
        post: Post[]
      },
      error: { message: string }
    }>(API_URL + 'Dashboard', { perPage, currentPage })
      .pipe(map(res => {
        if (res.error) {
          throwError(res.error);
        } else {
          return res.result.post;
        }
      }))
      .subscribe(
        (posts: Post[]) => {
          this.posts = posts;
          this.postsUpdated.next([...this.posts]);
          
        },
        err => {
          this.toastr.showError(err.message, err);
          
        }
      );
  }

  getPostsUpdateListener () {
    return this.postsUpdated.asObservable();
  }
}
