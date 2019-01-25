import {Component, OnInit} from '@angular/core';
import {ApiService} from '../../services/api.service';
import {Post} from '../../models/post.interface';

@Component({
  selector: 'app-posts',
  templateUrl: './posts.component.html',
  styleUrls: ['./posts.component.css']
})
export class PostsComponent implements OnInit {
  posts: Post[] = [];

  constructor(private apiService: ApiService) {}

  ngOnInit() {
     this.getPosts();
  }

  getPosts() {
    this.apiService.getPosts()
      .subscribe(
        (res: Post[]) => {
          this.posts = res;
        },
        err => {
          console.log(err);
        }
      );
  }
}
