import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { User } from '../models/user.model';
import { map } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private url = "http://localhost:8000/api";

  public user?: User;

  constructor(private connection: HttpClient) {
    this.loadUser()
  }

  public login(email: string, password: string) {
    return this.connection.post<{ token: string }>(`${this.url}/login`, {
      email,
      password,
    }).pipe(map((result) => {
      localStorage.setItem("token", result.token);
      this.getUser().subscribe()
      location.reload();
    }));
  }

  public logout() {
    return this.connection.post(
      `${this.url}/logout`,
      {},
      {
        headers: { Authorization: `Bearer ${localStorage.getItem("token")}` }
      })
      .pipe(map(() => {
        this.user = undefined;
        localStorage.removeItem('token');
        sessionStorage.removeItem('cart');
        location.reload();
      }))
  }

  public register(name: string, email: string, phone: string, password: string, passwordAgain: string) {
    return this.connection.post<{ token: string }>(`${this.url}/register`, {
      name,
      email,
      phone,
      password,
      password_confirmation: passwordAgain,
    });
  }

  public loadUser() {
    this.getUser().subscribe();
  }

  public getUser() {
    return this.connection.get<User>(
      "http://localhost:8000/api/user",
      {
        headers: {
          Authorization: `Bearer ${localStorage.getItem("token")}`
        }
      }
    ).pipe(map((user) => {
      this.user = user;
      return user;
    }))
  }

  public get isLoggedIn() {
    return !!this.user;
  }


  public update(data: Partial<{
    email: string,
    phone: string,
    password: string,
    current_password: string,
    password_confirmation: string,
  }>) {
    return this.connection.put<{ token: string }>(`${this.url}/user`, data,
      {
        headers: { Authorization: `Bearer ${localStorage.getItem("token")}` }
      })
  }

  public deleteUser(current_password: string) {
    return this.connection.delete(`${this.url}/user/${this.user!.id}`, {
      headers: { Authorization: `Bearer ${localStorage.getItem("token")}` },
      body: { current_password}
    })
  }
}






// import { HttpClient } from '@angular/common/http';
// import { Injectable } from '@angular/core';
// import { User } from '../models/user.model';
// import { map } from 'rxjs';
// import { CartService } from './cart.service';
// @Injectable({
//   providedIn: 'root'
// })
// export class AuthService {
//   private url = "http://localhost:8000/api";

//   public user?: User;

//   constructor(private connection: HttpClient) {
//     this.loadUser()

//   }

//   public login(email: string, password: string) {
//     return this.connection.post<{ token: string, user: { id: string } }>(`${this.url}/login`, {
//       email,
//       password,
//     }).pipe(map((result) => {
//       localStorage.setItem("token", result.token);
//       const decodedToken = this.decodeToken(result.token);
//       if (decodedToken && decodedToken.userId) {
//         localStorage.setItem("userId", decodedToken.userId);
//       }
//       this.getUser().subscribe();
//     }));
//   }
  

//   // public login(email: string, password: string) {
//   //   return this.connection.post<{ token: string, user: {id: number} }>(`${this.url}/login`, {
//   //     email,
//   //     password,
//   //   }).pipe(map((result) => {
//   //     localStorage.setItem("token", result.token);
//   //     localStorage.setItem("userId", result.user.id.toString());
//   //     this.getUser().subscribe()
//   //   }));
//   // }

//   public logout() {
//     return this.connection.post(
//       `${this.url}/logout`,
//       {},
//       {
//         headers: { Authorization: `Bearer ${localStorage.getItem("token")}` }
//       })
//       .pipe(map(() => {
//         this.user = undefined;
//         localStorage.removeItem('token');
//       }))
//   }

//   public register(name: string, email: string, phone: string, password: string, passwordAgain: string) {
//     return this.connection.post<{ token: string }>(`${this.url}/register`, {
//       name,
//       email,
//       phone,
//       password,
//       password_confirmation: passwordAgain,
//     });
//   }

//   private loadUser() {
//     this.getUser().subscribe();
//   }

//   public getUser() {
//     return this.connection.get<User>(
//       "http://localhost:8000/api/user",
//       {
//         headers: {
//           Authorization: `Bearer ${localStorage.getItem("token")}`
//         }
//       }
//     ).pipe(map((user) => {
//       this.user = user;
//       return user;
//     }))
//   }

//   public get isLoggedIn() {
//     return !!this.user;
//   }


//   public update(data: Partial<{
//     email: string,
//     phone: string,
//     password: string,
//     current_password: string,
//     password_confirmation: string,
//   }>) {
//     return this.connection.put<{ token: string }>(`${this.url}/user`, data,
//       {
//         headers: { Authorization: `Bearer ${localStorage.getItem("token")}` }
//       })
//   }

//   public deleteUser(current_password: string) {
//     return this.connection.delete(`${this.url}/user/${this.user!.id}`, {
//       headers: { Authorization: `Bearer ${localStorage.getItem("token")}` },
//       body: { current_password}
//     })
//   }

//   private decodeToken(token: string) {
//     try {
//       const tokenPayload = token.split('|')[1];
//       return tokenPayload;
//     } catch (error) {
//       console.error("Token decoding failed:", error);
//       return null;
//     }
//   }
// }
