import IUser from '@/models/IUser';

export default class UserDto implements IUser {
    public Login: string = '';
    public Password: string = '';
    public PublicId: string = '';
    public Name: string = '';
    public Lastname: string = '';
    public Gender: string = '';
    public Location: string = '';
    public Interests: string[] = [];
}
