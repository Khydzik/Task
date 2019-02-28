import { Component, OnDestroy, OnInit } from '@angular/core';
import { PageEvent } from '@angular/material';
import { Subscription } from 'rxjs';
import { PostsService } from '../../services/posts.service';
import { Post } from '../../models/post.interface';
import { NgxSpinnerService } from 'ngx-spinner';
import {DomSanitizer} from '@angular/platform-browser';

@Component({
  selector: 'app-posts',
  templateUrl: './posts.component.html',
  styleUrls: ['./posts.component.css']
})
export class PostsComponent implements OnInit, OnDestroy { 
  posts: Post[] = [];
  totalPosts = 0;
  take = 2;
  skip = 1;
  url = "http://localhost:5000/Posts/Images/";
  pageSizeOptions = [1, 2, 5, 10];
  private postsSub: Subscription;

  constructor(
    public sanitizer: DomSanitizer,
    private postsService: PostsService,
    private spinner: NgxSpinnerService,
  ) {}

  ngOnInit() {
    this.spinner.show();
    this.postsService.getPosts(this.take, this.skip);
    this.postsSub = this.postsService.getPostsUpdateListener()
      .subscribe(
        (posts: Post[]) => {
          this.spinner.hide();
          this.posts = posts;
          this.totalPosts = this.posts.length;
        }
      );
  }

  onChangedPage (pageData: PageEvent) {
    this.spinner.show();
    this.skip = pageData.pageIndex + 1;
    this.take = pageData.pageSize;
    this.postsService.getPosts(this.take, this.skip);
  }

  ngOnDestroy (): void {
    this.postsSub.unsubscribe();
  }
}
