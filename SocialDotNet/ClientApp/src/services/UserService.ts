import UserDto from '@/models/userdto';
// @ts-ignore
import * as uuid from 'uuid';
import IToken from '@/models/token';
import IUser from '@/models/IUser';
import LoginDto from '@/models/LoginDto';

export default class UserService {

    public endpoint: string = '/api/user'; // "https://social-dot-net.herokuapp.com/api/user";

    public CurrentUserId(): string | null {
        const tokenJson = localStorage.getItem('Token');
        if (tokenJson == null) {
            return null;
        }
        const token = JSON.parse(tokenJson) as IToken;
        return token.publicId;
    }

    public Logout(): void {
        localStorage.removeItem('Token');
    }

    public InitHeaders(anonymous: boolean): HeadersInit {
        const requestHeaders: HeadersInit = new Headers();
        requestHeaders.set('Content-Type', 'application/json');
        if (!anonymous) {
            const tokenJson = localStorage.getItem('Token');
            if (tokenJson == null) {
                throw new Error('No user token');
            }
            const token = JSON.parse(tokenJson) as IToken;
            requestHeaders.set('authorization', `Bearer ${token.token}`);
        }
        return requestHeaders;
    }

    public async Register(newUser: UserDto): Promise<boolean> {
        newUser.PublicId = uuid.v4();
        const requestHeaders = this.InitHeaders(true);
        return await fetch(`${this.endpoint}/register`, {
                        method: 'post',
                        headers: requestHeaders,
                        body: JSON.stringify(newUser),
                    })
                    .then((result: Response) => {
                        return result.status == 200;
                    })
                    .catch(() => false);
    }

    public async Login(data: LoginDto): Promise<string> {
        const requestHeaders = this.InitHeaders(true);
        const result = await fetch(`${this.endpoint}/authenticate`, {
                        method: 'post',
                        headers: requestHeaders,
                        body: JSON.stringify(data),
                    })
                    .then(async (response: Response) => {
                        return await this.ReadLogin(response);
                    });
        return result;
    }
    public async ReadLogin(response: Response): Promise<string> {
        if (response.status !== 200) {
            throw new Error('Please try again');
        }

        const token = await response.json() as IToken;
        localStorage.setItem('Token', JSON.stringify(token));
        return token.publicId;
    }

    public async GetUser(publicId: string | null): Promise<IUser | null> {
        if (publicId === null || publicId.length === 0) {
            publicId = this.CurrentUserId();
        }
        if (publicId === null) {
            throw new Error('User unknown');
        }
        const requestHeaders = this.InitHeaders(false);
        const result = await fetch(`${this.endpoint}/${publicId}`, {
                        method: 'get',
                        headers: requestHeaders,
                    })
                    .then(async (response: Response) => {
                        if (response.status == 200) {
                            return (await response.json()) as IUser;
                        } else {
                            return null;
                        }
                    });
        return result;
    }
}
