import { DOCUMENT } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Component, Inject } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { AuthService } from '@auth0/auth0-angular';

@Component({
  selector: 'app-link-ptcgo-account',
  template: `
    <div *ngIf="accountId===null">
      <p>AccountId was not provided!</p>
    </div>
    <div *ngIf="accountLinked">
      <p>Your PTCGO account was linked!</p>
    </div>
    <div *ngIf="accountLinkFailed">
      <p>Something went wrong, please try again.</p>
    </div>
  `,
  styles: [],
})
export class LinkPtcgoAccountComponent {

  accountId: string | null;
  accountLinked: boolean = false;
  accountLinkFailed: boolean = false;
  userEmail: string | null = null;

  httpClient : HttpClient;

  constructor(private route: ActivatedRoute, private http : HttpClient, public auth: AuthService) {
    this.httpClient = http;
    this.accountId = null;
  }

  ngOnInit() {
    this.auth.user$.subscribe(
      (profile) => {
        this.userEmail = profile.email;

        this.tryLinkAccount();
      }
    );

    const providedAccountId = this.route.snapshot.queryParams['accountId'];
    if (providedAccountId !== undefined && providedAccountId !== '')
      this.accountId = providedAccountId;

      this.tryLinkAccount();
  }

  private tryLinkAccount() {
    if (this.accountId !== null && this.userEmail != null) {
      this.httpClient.post("api/users/linkAccount", {
        email: this.userEmail,
        accountId: this.accountId
      })
      .subscribe((result : any) => {
        this.accountLinked = true;
      },
      (error : any) => {
        this.accountLinkFailed = true;
        console.log(error);
      });
    }
  }
}