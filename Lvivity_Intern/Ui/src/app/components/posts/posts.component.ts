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
  perPage = 2;
  currentPage = 1;
  url = "http://localhost/Posts/";
  pageSizeOptions = [1, 2, 5, 10];
  private postsSub: Subscription;

  constructor(
    public sanitizer: DomSanitizer,
    private postsService: PostsService,
    private spinner: NgxSpinnerService,
  ) {}

  ngOnInit() {
    this.spinner.show();
    this.postsService.getPosts(this.perPage, this.currentPage);
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
    this.currentPage = pageData.pageIndex + 1;
    this.perPage = pageData.pageSize;
    this.postsService.getPosts(this.perPage, this.currentPage);
  }

  ngOnDestroy (): void {
    this.postsSub.unsubscribe();
  }
}
