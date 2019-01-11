import {Component, OnDestroy, OnInit} from '@angular/core';
import {ApiService} from '../../services/api.service';
import {Post} from '../../models/post.interface';
import {Subscription} from 'rxjs';

@Component({
  selector: 'app-posts',
  templateUrl: './posts.component.html',
  styleUrls: ['./posts.component.css']
})
export class PostsComponent implements OnInit, OnDestroy {
  posts: Post[] = [];

  postsSub: Subscription;

  constructor(private apiService: ApiService) {}

  ngOnInit() {
    this.getPosts();
  }

  getPosts() {
    this.postsSub = this.apiService.getPosts()
      .subscribe(
        (res: Post[]) => {
          console.log(res);
          this.posts = res;
        },
        err => {
          console.log(err);
        }
      );
  }

  ngOnDestroy() {
    this.postsSub.unsubscribe();
  }
}
