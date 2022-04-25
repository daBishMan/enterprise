import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'ngx-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  title = 'ngx-app';

  ngOnInit() {
    // eslint-disable-next-line no-restricted-syntax
    console.info(`AppComponent.ngOnInit()`);
  }
}
